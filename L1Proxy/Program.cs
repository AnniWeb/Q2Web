using L1Proxy.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Threading;

namespace L1Proxy
{
    class Program
    {
        private static readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string _fileName = "result";
        private static readonly string _ext = "txt";
        
        static async Task Main(string[] args)
        {
            var filePath = $"{_fileName}.{_ext}";
            var readedTasks = new List<Task<Post>>();
            var text = new List<string>();

            try
            {
                // Выполнение всего кода прервется через 30 секунд
                _cts.CancelAfter(30000);
                for (var i = 4; i <= 13; i++)
                {
                    readedTasks.Add(GetPostAsync(i));
                }
                
                // Ожидаем получения всех постов
                await Task.WhenAll(readedTasks);
                
                // Собираем посты в один массив
                readedTasks.ForEach(task =>
                {
                    var post = task.Result;
                    if (post.Valid())
                    {
                        text.Add(post.UserId.ToString());
                        text.Add(post.Id.ToString());
                        text.Add(post.Title);
                        text.Add(post.Body);
                        text.Add(String.Empty);
                    }
                    else
                    {
                        throw new ApplicationException("Получен некорректый пост");
                    }
                });

                // Асинхронно сохраняем все данные разом
                await File.WriteAllLinesAsync(filePath, text, _cts.Token);
                Console.WriteLine("Данные записаны");
            }
            catch (ApplicationException e)
            {
                // сработало прерывание по таймауту
                Console.WriteLine($"Выполнение было прервано: {e.Message}");
            }
            catch (TaskCanceledException)
            {
                // сработало прерывание по таймауту
                Console.WriteLine("Выполнение было прервано");
            }
            finally
            {
                _cts.Dispose();
            }
        }

        /// <summary>
        /// Асинхронное получение данных о посте
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static async Task<Post> GetPostAsync(int id)
        {
            var post = new Post();
            var url = $"https://jsonplaceholder.typicode.com/posts/{id}";
            
            while (!post.Valid())
            {
                try
                {
                    // получаем данные
                    HttpResponseMessage response = await _client.GetAsync(url, _cts.Token);
                    response.EnsureSuccessStatusCode();
                    
                    // разбираем данные
                    string responseBody = await response.Content.ReadAsStringAsync(_cts.Token);
                    post = JsonSerializer.Deserialize<Post>(responseBody);
                }
                catch (HttpRequestException)
                {
                    // Если данные не получены, ожидаем 1 секунду и повторяем запрос
                    await Task.Delay(1000);
                }
                
                if (_cts.IsCancellationRequested)
                {
                    throw new ApplicationException($"Неудалось получить данные по {url}");
                }
            }
            
            return post;
        }
    }
}

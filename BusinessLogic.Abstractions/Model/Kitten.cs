﻿namespace BusinessLogic.Abstractions.Model
{
    public class Kitten : BaseModel<int>
    {
        public string NickName { get; set; }
        
        public int Weigth { get; set; }
        
        public string Color { get; set; }
        
        public bool HasCertificate { get; set; }
        
        public string Feed { get; set; }
    }
}
﻿namespace MatchDayApp.Domain.Configuration
{
    public class DatabaseOptions
    {
        public string ConnectionString { get; set; }
        public bool UseInMemoryDatabase { get; set; }
    }
}

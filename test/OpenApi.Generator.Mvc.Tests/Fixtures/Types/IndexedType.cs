﻿using System;

namespace OpenApi.Generator.Mvc.Tests
{
    public class IndexedType
    {
        public decimal Property1 { get; set; }

        public string this[string key1]
        {
            get { throw new NotImplementedException(); }
        }

        public string this[int key2]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OpenApi.Generator.Mvc.Tests
{
    public class BindingAnnotatedType
    {
        public string StringWithNoAttributes { get; set; }

        [BindRequired]
        public string StringWithBindRequired { get; set; }

        [BindRequired]
        public int IntWithBindRequired { get; set; }

        [BindNever]
        public int IntWithBindNever { get; set; }
    }
}

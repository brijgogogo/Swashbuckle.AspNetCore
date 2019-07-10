using System;
using Xunit;
using Xunit.Abstractions;

namespace OpenApi.Generator.Mvc.Tests
{
    public class XmlCommentsMemberNameHelperTests
    {
        private readonly ITestOutputHelper _output;

        public XmlCommentsMemberNameHelperTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(typeof(XmlAnnotatedType), "AcceptsNothing",
            "M:OpenApi.Generator.Mvc.Tests.XmlAnnotatedType.AcceptsNothing")]
        [InlineData(typeof(XmlAnnotatedType), "AcceptsNestedType",
            "M:OpenApi.Generator.Mvc.Tests.XmlAnnotatedType.AcceptsNestedType(OpenApi.Generator.Mvc.Tests.XmlAnnotatedType.NestedType)")]
        [InlineData(typeof(XmlAnnotatedType), "AcceptsConstructedGenericType",
            "M:OpenApi.Generator.Mvc.Tests.XmlAnnotatedType.AcceptsConstructedGenericType(System.Collections.Generic.KeyValuePair{System.String,System.Int32})")]
        [InlineData(typeof(XmlAnnotatedType), "AcceptsConstructedOfConstructedGenericType",
            "M:OpenApi.Generator.Mvc.Tests.XmlAnnotatedType.AcceptsConstructedOfConstructedGenericType(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,System.Int32}})")]
        [InlineData(typeof(XmlAnnotatedType), "AcceptsArrayOfConstructedGenericType",
            "M:OpenApi.Generator.Mvc.Tests.XmlAnnotatedType.AcceptsArrayOfConstructedGenericType(System.Nullable{System.Int32}[])")]
        public void GetMemberNameForMethod_ReturnsCorrectXmlCommentsMemberName_ForGivenMethodInfo(
            Type declaringType, 
            string name,
            string expectedMemberName)
        {
            var methodInfo = declaringType.GetMethod(name);

            var memberName = XmlCommentsMemberNameHelper.GetMemberNameForMethod(methodInfo);

            _output.WriteLine(expectedMemberName);
            _output.WriteLine(memberName);
            Assert.Equal(expectedMemberName, memberName);
        }

        [Theory]
        [InlineData(typeof(XmlAnnotatedType),
            "T:OpenApi.Generator.Mvc.Tests.XmlAnnotatedType")]
        [InlineData(typeof(XmlAnnotatedType.NestedType),
            "T:OpenApi.Generator.Mvc.Tests.XmlAnnotatedType.NestedType")]
        [InlineData(typeof(XmlAnnotatedGenericType<int,string>),
            "T:OpenApi.Generator.Mvc.Tests.XmlAnnotatedGenericType`2")]
        [InlineData(typeof(NoNamespaceType),
            "T:NoNamespaceType")]
        public void GetMemberNameForType_ReturnsCorrectXmlCommentsMemberName_ForGivenType(
            Type type,
            string expectedMemberName
        )
        {
            var memberName = XmlCommentsMemberNameHelper.GetMemberNameForType(type);

            _output.WriteLine(expectedMemberName);
            _output.WriteLine(memberName);
            Assert.Equal(expectedMemberName, memberName);
        }

        [Theory]
        [InlineData(typeof(XmlAnnotatedType), "StringProperty",
            "P:OpenApi.Generator.Mvc.Tests.XmlAnnotatedType.StringProperty")]
        [InlineData(typeof(XmlAnnotatedType), "StringField",
            "F:OpenApi.Generator.Mvc.Tests.XmlAnnotatedType.StringField")]
        [InlineData(typeof(XmlAnnotatedType.NestedType), "Property",
            "P:OpenApi.Generator.Mvc.Tests.XmlAnnotatedType.NestedType.Property")]
        [InlineData(typeof(XmlAnnotatedGenericType<int,string>), "GenericProperty",
            "P:OpenApi.Generator.Mvc.Tests.XmlAnnotatedGenericType`2.GenericProperty")]
        public void GetMemberNameForProperty_ReturnsCorrectXmlCommentMemberName_ForGivenMemberInfo(
            Type declaringType,
            string fieldOrPropertyName,
            string expectedMemberName
        )
        {
            var memberInfo = declaringType.GetMember(fieldOrPropertyName)[0];

            var memberName = XmlCommentsMemberNameHelper.GetMemberNameForMember(memberInfo);

            _output.WriteLine(expectedMemberName);
            _output.WriteLine(memberName);
            Assert.Equal(expectedMemberName, memberName);
        }
    }
}

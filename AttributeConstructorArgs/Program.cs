var attributeData = typeof(TestClass).GetCustomAttributesData()[0];
Console.WriteLine(attributeData.AttributeType == typeof(TestAttribute)); // true
Console.WriteLine(attributeData.ConstructorArguments[0].Value); // "Hello World"

[Test("Hello, world!")]
public class TestClass
{    
}

public class TestAttribute : Attribute
{
    public TestAttribute(string greeting) { }
}
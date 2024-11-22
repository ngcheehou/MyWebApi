namespace MyWebApi
{


    public class MyCustomException : ApplicationException
    {
        public MyCustomException(string message) : base(message) { }
    }
}


namespace mynamespace{
    delegate void delegate1();
    delegate void delegate2(string num1);
    delegate void delegate3(int temp);

    class book
    {
        static void display()
        {
            Console.WriteLine("Pakistan Zindabad");
        }
            
        static void display(int num1)
        {
            Console.WriteLine("I Love UET KSK");
        }
        static void display(string temp)
        {
            Console.WriteLine("Best of Luck");
        }

        public static void Main(string[] args)
        {
            delegate1 obj1 = new delegate1(display);
            delegate2 obj2 = new delegate2(display);
            delegate3 obj3 = new delegate3(display);

            obj1.Invoke();
            obj1();
            /*
                Output:
                Pakistan Zindabad
                Pakistan Zindabad
            */

            obj1();
            obj2("Hello");
            obj3(5);

            /*
                Output:
                Pakistan Zindabad
                Best of Luck
                I Love UET KSK
            */
        }
    }
}
using Business_Objects;
namespace Presentation_Layer
{
    public class PL
    {
        public Emp getInput(Emp emp)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Age");
                    emp.Age = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            return emp;
        }
        public void display(Emp emp)
        {
            Console.WriteLine("Age: " + emp.Age);
            Console.WriteLine("Salary: " + emp.Salary);
        }
    }
}

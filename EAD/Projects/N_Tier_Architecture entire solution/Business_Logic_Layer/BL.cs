using Business_Objects;

namespace Business_Logic_Layer
{
    public class BL
    {
        public Emp calculateSalary(Emp emp)
        {
            if (emp.Age > 30)
            {
                emp.Salary = 50000;
            }
            else
            {
                emp.Salary = 30000;
            }
            return emp;
        }
    }
}

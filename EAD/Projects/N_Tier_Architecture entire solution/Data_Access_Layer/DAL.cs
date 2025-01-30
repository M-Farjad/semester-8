using Business_Objects;

namespace Data_Access_Layer
{
    public class DAL
    {
        public bool save(Emp emp)
        {
            if (emp.Age > 0 && emp.Salary > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

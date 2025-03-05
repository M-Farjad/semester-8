using Bussiness_objects;
namespace Bussinesslogic_layer
{
	public class BL
	{
		public Emp calculate(Emp obj)
		{
			if (obj.Age > 30)
			{
				obj.Salary = 500000;
			}
			else
			{
				obj.Salary = 300000;
			}

			return obj;
		}

	}
}

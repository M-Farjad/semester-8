using Bussiness_objects;
namespace Presentation_layer
{
	public class PL
	{
		public Emp getinput( Emp obj)
		{
			Console.WriteLine("Enter age");
			obj.Age = Convert.ToInt32( Console.ReadLine());

			return obj;
		}
	}
}

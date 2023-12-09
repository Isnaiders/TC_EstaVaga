namespace BlazorMauiApp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			// Registrar a rota
			Routing.RegisterRoute("Parking/View/{id}", typeof(RazorClassLibrary.Pages.Parking.View));

			MainPage = new MainPage();
		}
	}
}
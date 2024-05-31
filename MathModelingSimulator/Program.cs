using Avalonia;
using Avalonia.ReactiveUI;
using System;

namespace MathModelingSimulator
{
	internal sealed class Program
	{
		public static string HostNpgsql = "Host=ngknn.ru;Port=5442;Database=trio_33p;Username=33P;Password=12345";

		/*public static string HostNpgsql = "Host=edu.pg.ngknn.local;Port=5432;Database=trio_33p;Username=33P;Password=12345";*/

		// Initialization code. Don't use any Avalonia, third-party APIs or any
		// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
		// yet and stuff might break.
		[STAThread]
		public static void Main(string[] args) => BuildAvaloniaApp()
			.StartWithClassicDesktopLifetime(args);

		// Avalonia configuration, don't remove; also used by visual designer.
		public static AppBuilder BuildAvaloniaApp()
			=> AppBuilder.Configure<App>()
				.UsePlatformDetect()
				.WithInterFont()
				.LogToTrace()
				.UseReactiveUI();
	}
}

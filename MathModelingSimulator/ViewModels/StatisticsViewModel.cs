using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView.Drawing;
using DialogHostAvalonia;
using MathModelingSimulator.Views;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;

namespace MathModelingSimulator.ViewModels
{
	public class StatisticsViewModel : MainWindowViewModel
	{
		private ISeries[] series = new ISeries[0];
		public ISeries[] Series { get => series; set => SetProperty(ref series, value); }
		public StatisticsViewModel()
		{
			//*TO DO* Получение статистики из БД и вывод по ней диаграммы
			Series = new ISeries[]
			{
				new PieSeries<int>
				{
					Name = "Правильные ответы",
					Values = new int[] { 2 },
					Fill = (IPaint<SkiaSharpDrawingContext>) new SolidColorPaint(SKColor.Parse("#09A0B3")),
					/*InnerRadius = 50 (для пончика)*/
				},
				new PieSeries<int>
				{
					Name = "Неправильные ответы",
					Values = new int[] { 4 },
					Fill = (IPaint<SkiaSharpDrawingContext>) new SolidColorPaint(SKColor.Parse("#7B69E1"))
				}
			};		
		}		
	}
}
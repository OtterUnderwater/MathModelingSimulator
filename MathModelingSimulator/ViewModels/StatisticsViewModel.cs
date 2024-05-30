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
using Avalonia.Controls;
using System.Linq;
using MathModelingSimulator.Models;
using System.Collections.Generic;

namespace MathModelingSimulator.ViewModels
{
	public class StatisticsViewModel : MainWindowViewModel
	{
		private ISeries[] series = new ISeries[0];
		public ISeries[] Series { get => series; set => SetProperty(ref series, value); }
		public StatisticsViewModel()
		{
			//*TO DO* Проверка, что history != null или скрываем видимость
			List<History> history = ContextDb.Histories.Where(h => h.IdUser == CurrentUser.Id).ToList();
			int countFalse = history.Count(h => h.Result == false);
			int countTrue = history.Count(h => h.Result == true);
			Series = new ISeries[]
			{
				new PieSeries<int>
				{
					Name = $"Правильные ответы: {countTrue}",
					Values = new int[] { countTrue },
					Fill = (IPaint<SkiaSharpDrawingContext>) new SolidColorPaint(SKColor.Parse("#09A0B3")),
					/*InnerRadius = 50 (для пончика)*/
				},
				new PieSeries<int>
				{
					Name = $"Неправильные ответы: {countFalse}",
					Values = new int[] { countFalse },
					Fill = (IPaint<SkiaSharpDrawingContext>) new SolidColorPaint(SKColor.Parse("#7B69E1"))
				}
			};
		}
	}
}
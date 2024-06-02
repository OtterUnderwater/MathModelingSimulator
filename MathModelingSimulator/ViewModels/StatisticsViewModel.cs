using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView.Drawing;
using System.Linq;
using MathModelingSimulator.Models;
using System.Collections.Generic;
using System;
using DynamicData;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MathModelingSimulator.ViewModels
{
	public class StatisticsViewModel : MainWindowViewModel
	{
		#region PropertyObjects
		private ISeries[] series = new ISeries[0];
		public ISeries[] Series { get => series; set => SetProperty(ref series, value); }

		bool isVisiblePieChart = true;
		public bool IsVisiblePieChart
		{
			get => isVisiblePieChart;
			set => SetProperty(ref isVisiblePieChart, value);
		}

		bool isVisibleTextBlockNull = false;
		public bool IsVisibleTextBlockNull
		{
			get => isVisibleTextBlockNull;
			set => SetProperty(ref isVisibleTextBlockNull, value);
		}

		bool isVisibleStudent = false;
		public bool IsVisibleStudent
		{
			get => isVisibleStudent;
			set => SetProperty(ref isVisibleStudent, value);
		}

		bool isVisibleTeacher = false;
		public bool IsVisibleTeacher
		{
			get => isVisibleTeacher;
			set => SetProperty(ref isVisibleTeacher, value);
		}

		bool isVisibleDataGrid = false;
		public bool IsVisibleDataGrid
		{
			get => isVisibleDataGrid;
			set => SetProperty(ref isVisibleDataGrid, value);
		}

		List<HistoryUser> allHistory = new List<HistoryUser>();
		public List<HistoryUser> AllHistory
		{
			get => allHistory;
			set => SetProperty(ref allHistory, value);
		}
		#endregion

		public StatisticsViewModel()
		{
			if (ContextDb.Roles.First(it => it.IdRole == CurrentUser.IdRole).Role1 == "Студент")
			{
				UserStudent();
				IsVisibleStudent = true;
				IsVisibleTeacher = false;
			}
			else
			{
				UserTeacher();
				IsVisibleTeacher = true;
				IsVisibleStudent = false;
			}
		}

		public void UserStudent()
		{
			List<History> history = ContextDb.Histories.Where(h => h.IdUser == CurrentUser.Id).ToList();
			if (history.Count > 0)
			{
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
				IsVisiblePieChart = true;
				IsVisibleTextBlockNull = false;
			}
			else
			{
				IsVisiblePieChart = false;
				IsVisibleTextBlockNull = true;
			}
		}

		public void UserTeacher()
		{
			List<History> history = ContextDb.Histories.ToList();
			if (history.Count > 0)
			{
				foreach (History h in history)
				{
					HistoryUser historyUser = new HistoryUser();
					historyUser.PassageDateTime = h.PassageDateTime;
					historyUser.Surname = ContextDb.Users.First(it => it.Id == h.IdUser).Surname;
					historyUser.Name = ContextDb.Users.First(it => it.Id == h.IdUser).Name;
					historyUser.Login = ContextDb.Users.First(it => it.Id == h.IdUser).Login;
					historyUser.Result = h.Result ? "Успешно" : "Неправильно";
					historyUser.NameSimulator = ContextDb.Simulators.First(it => it.Id == h.IdSimulator).Name;
					AllHistory.Add(historyUser);
				}
				IsVisibleDataGrid = true;
				IsVisibleTextBlockNull = false;
			}
			else
			{
				IsVisibleDataGrid = false;
				IsVisibleTextBlockNull = true;
			}
		}
	}

	public class HistoryUser
	{
		public DateTime PassageDateTime { get; set; }
		public string Surname { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string Login { get; set; } = null!;
		public string Result { get; set; } = null!;
		public string NameSimulator { get; set; } = null!;
	}
}
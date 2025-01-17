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

namespace MathModelingSimulator.ViewModels
{
	public class StatisticsViewModel : MainWindowViewModel
	{
		#region PropertyObjects
		private ISeries[] seriesPieChart = new ISeries[0];
		public ISeries[] SeriesPieChart { get => seriesPieChart; set => SetProperty(ref seriesPieChart, value); }

		private ISeries[] seriesCartesianChart = new ISeries[0];
		public ISeries[] SeriesCartesianChart { get => seriesCartesianChart; set => SetProperty(ref seriesCartesianChart, value); }

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

		List<Axis> xAxes = new List<Axis>();
		public List<Axis> XAxes { get => xAxes; set => SetProperty(ref xAxes, value); }
		#endregion

		public StatisticsViewModel()
		{
			if (ContextDb.Roles.First(it => it.IdRole == UserRole).Role1 == "�������")
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
				SeriesPieChart = new ISeries[]
				{
				new PieSeries<int>
				{
					Name = $"���������� ������: {countTrue}",
					Values = new int[] { countTrue },
					Fill = (IPaint<SkiaSharpDrawingContext>) new SolidColorPaint(SKColor.Parse("#09A0B3"))
				},
				new PieSeries<int>
				{
					Name = $"������������ ������: {countFalse}",
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
				List<History> distinctUsers = history.DistinctBy(h => h.IdUser).ToList();
				List<ISeries> series = new List<ISeries>();
				List<DateTime> dateHistory = history.Select(h => h.PassageDateTime.Date).Distinct().OrderBy(date => date).ToList(); // ��������� ���������� ���
				Axis axis = new Axis();
				string[] stringsDate = new string[dateHistory.Count];
				for (int i = 0; i < dateHistory.Count; i++)
				{
					stringsDate[i] = dateHistory[i].Date.ToString("dd.MM.yyyy");
				}
				axis.Labels = stringsDate;
				XAxes.Add(axis);
				foreach (var user in distinctUsers)
				{
					LineSeries<int> lineSeries = new LineSeries<int>();
					User userInfo = ContextDb.Users.FirstOrDefault(u => u.Id == user.IdUser);
					lineSeries.Name = $"{userInfo.Surname} {userInfo.Name}";
					List<History> userHistory = history.Where(h => h.IdUser == user.IdUser).OrderBy(h => h.PassageDateTime).ToList();
					List<int> values = new List<int>();
					for (int i = 0; i < dateHistory.Count; i++)
					{
						int testsPassed = userHistory.Count(u => u.PassageDateTime.Date == dateHistory[i].Date);
						//������� ������ ������ ������������ � ����
						values.Add(testsPassed);
					}
					lineSeries.Values = values.ToArray();
					series.Add(lineSeries);
				}
				SeriesCartesianChart = series.ToArray();
				foreach (History h in history)
				{
					HistoryUser historyUser = new HistoryUser();
					historyUser.PassageDateTime = h.PassageDateTime;
					historyUser.Surname = ContextDb.Users.First(it => it.Id == h.IdUser).Surname;
					historyUser.Name = ContextDb.Users.First(it => it.Id == h.IdUser).Name;
					historyUser.Login = ContextDb.Users.First(it => it.Id == h.IdUser).Login;
					historyUser.Result = h.Result ? "�������" : "�����������";
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
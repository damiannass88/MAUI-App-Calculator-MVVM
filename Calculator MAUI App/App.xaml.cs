﻿namespace Calculator_MAUI_App;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new AppShell());
	}
}

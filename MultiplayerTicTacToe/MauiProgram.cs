using Microsoft.Extensions.Logging;
using MultiplayerTicTacToe.View;
using MultiplayerTicTacToe.ViewModel;

namespace MultiplayerTicTacToe;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<GamePageVM>();
		builder.Services.AddSingleton<HiscorePageVM>();
		builder.Services.AddSingleton<PlayerPageVM>();

		builder.Services.AddTransient<GamePage>();
		builder.Services.AddTransient<HiScorePage>();
		builder.Services.AddTransient<PlayerPage>();

		return builder.Build();
	}
}

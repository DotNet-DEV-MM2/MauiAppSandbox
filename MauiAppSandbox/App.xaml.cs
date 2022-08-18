using MauiAppSandbox.Services;

namespace MauiAppSandbox;

public partial class App : Application
{
    //public static ClosetItemSQLiteRepository _closetItemRepository { get; private set; }
    public App(ClosetItemSQLiteRepository closetItemRepository)
	{
		InitializeComponent();

		MainPage = new AppShell();

       // _closetItemRepository = closetItemRepository;

    }
}

using MauiAppSandbox.Services;

namespace MauiAppSandbox;

public partial class App : Application
{
    public static CategoryService _categoryService { get; private set; }
    public static ClosetItemService _closetItemService { get; private set; }

    public App(CategoryService categoryService, ClosetItemService closetItemService)
	{
		InitializeComponent();

		MainPage = new AppShell();

        _categoryService = categoryService;
        _closetItemService = closetItemService;

    }
}

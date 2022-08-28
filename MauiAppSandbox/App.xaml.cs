using MauiAppSandbox.Interfaces;

namespace MauiAppSandbox;

public partial class App : Application
{
    //public static ICategoryRepository _categoryRepository { get; private set; }
    // public static IClosetItemRepository _closetItemRepository { get; private set; }

    /*public App(ICategoryRepository categoryRepository, IClosetItemRepository closetItemRepository)*/
    public App()
    {
		InitializeComponent();

		MainPage = new AppShell();

       /* _categoryRepository = categoryRepository;
        _closetItemRepository = closetItemRepository;*/

    }
}

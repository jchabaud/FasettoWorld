using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace FasettoWord
{
    public class WindowViewModel : BaseViewModel
    {

        private Window mWindows;

        private int mOuterMarginSize = 10;
        private int mWindowRadius = 10;


        public int ResizeBorder { get; set; } = 6;

        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        public int OuterMarginSize
        {
            get { return mWindows.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize; }
            set { mOuterMarginSize = value; }
        }

        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        public int WindowRadius
        {
            get { return mWindows.WindowState == WindowState.Maximized ? 0 : mWindowRadius; }
            set { mWindowRadius = value; }
        }

        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        public int TitleHeight { get; set; } = 42;

        public GridLength TitleHeightGridLength {  get { return new GridLength(TitleHeight + ResizeBorder); } }

        public double WindowMinimumWidth { get; set; } = 400;
        public double WindowMaximumHeight { get; set; } = 400;

        public Thickness InnerContentPadding { get { return new Thickness(ResizeBorder); } }
        public WindowViewModel(Window window)
        {
            mWindows = window;

            mWindows.StateChanged += (sender, e) =>
            {
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowCornerRadius));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(WindowRadius));
            };

            MinimizeCommand = new RelayCommand(() => mWindows.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindows.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindows.Close());
            MenuCommand = new RelayCommand(() =>SystemCommands.ShowSystemMenu(mWindows, GetMousePosition()));

            var resizer = new WindowResizer(mWindows);

        }

        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand MenuCommand { get; set; }

        
        public Point GetMousePosition()
        {
            var position = Mouse.GetPosition(mWindows);
            return new Point(position.X + mWindows.Left, position.Y + mWindows.Top);
        }

    }
}

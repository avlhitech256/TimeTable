using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Common.Data.Notifier;

namespace Common.ViewModel
{
    public class MenuItemStyle : Notifier
    {
        #region Members

        private Brush notSelectedAndMouseIsNotOverBrush;
        private Brush notSelectedAndMouseIsOverBrush;
        private Brush selectedAndMouseIsNotOverBrush;
        private Brush selectedAndMouseIsOverBrush;

        private Point renderTransformOrigin;
        private Brush background;
        private Cursor cursor;
        private bool selected;
        private bool isMouseOver;

        #endregion

        #region Constructors

        public MenuItemStyle() : this(null, null, null, null)
        {
        }

        public MenuItemStyle(Brush notSelectedAndMouseIsNotOverBrush,
                             Brush notSelectedAndMouseIsOverBrush,
                             Brush selectedAndMouseIsNotOverBrush,
                             Brush selectedAndMouseIsOverBrush)
        {
            this.notSelectedAndMouseIsNotOverBrush = notSelectedAndMouseIsNotOverBrush;
            this.notSelectedAndMouseIsOverBrush = notSelectedAndMouseIsOverBrush;
            this.selectedAndMouseIsNotOverBrush = selectedAndMouseIsNotOverBrush;
            this.selectedAndMouseIsOverBrush = selectedAndMouseIsOverBrush;
            SetDefaultBrush();

            renderTransformOrigin = new Point(0.5, 0.5);
            background = notSelectedAndMouseIsNotOverBrush;
            cursor = Cursors.Hand;
            selected = false;
            isMouseOver = false;
        }

        #endregion

        #region Properties

        public Brush NotSelectedAndMouseIsNotOverBrush
        {
            get
            {
                return notSelectedAndMouseIsNotOverBrush;
            }

            set
            {
                if (notSelectedAndMouseIsNotOverBrush == null || notSelectedAndMouseIsNotOverBrush.Equals(value))
                {
                    notSelectedAndMouseIsNotOverBrush = value;
                    OnPropertyChanged();
                    ChangeBackgroundBrush();
                }
            }
        }

        public Brush NotSelectedAndMouseIsOverBrush
        {
            get
            {
                return notSelectedAndMouseIsOverBrush;
            }

            set
            {
                if (notSelectedAndMouseIsOverBrush == null || notSelectedAndMouseIsOverBrush.Equals(value))
                {
                    notSelectedAndMouseIsOverBrush = value;
                    OnPropertyChanged();
                    ChangeBackgroundBrush();
                }

            }

        }

        public Brush SelectedAndMouseIsNotOverBrush
        {
            get
            {
                return selectedAndMouseIsNotOverBrush;
            }

            set
            {
                if (selectedAndMouseIsNotOverBrush == null || selectedAndMouseIsNotOverBrush.Equals(value))
                {
                    selectedAndMouseIsNotOverBrush = value;
                    OnPropertyChanged();
                    ChangeBackgroundBrush();
                }

            }

        }

        public Brush SelectedAndMouseIsOverBrush
        {
            get
            {
                return selectedAndMouseIsOverBrush;
            }

            set
            {
                if (selectedAndMouseIsOverBrush == null || selectedAndMouseIsOverBrush.Equals(value))
                {
                    selectedAndMouseIsOverBrush = value;
                    OnPropertyChanged();
                    ChangeBackgroundBrush();
                }

            }

        }

        public bool IsMouseOver
        {
            get
            {
                return isMouseOver;
            }

            set
            {
                if (isMouseOver != value)
                {
                    isMouseOver = value;
                    OnPropertyChanged();
                    ChangeBackgroundBrush();
                }

            }

        }

        public bool Selected
        {
            get
            {
                return selected;
            }

            set
            {
                if (selected != value)
                {
                    selected = value;
                    OnPropertyChanged();
                    ChangeBackgroundBrush();
                }

            }

        }

        public Point RenderTransformOrigin
        {
            get
            {
                return renderTransformOrigin;
            }

            set
            {
                if (renderTransformOrigin == null || !renderTransformOrigin.Equals(value))
                {
                    renderTransformOrigin = value;
                    OnPropertyChanged();
                }

            }

        }

        public Brush Background
        {
            get
            {
                return background;
            }

            set
            {
                if (background == null || !background.Equals(value))
                {
                    background = value;
                    OnPropertyChanged();
                }

            }

        }

        public Cursor Cursor
        {
            get
            {
                return cursor;
            }

            set
            {
                if (cursor == null || !cursor.Equals(value))
                {
                    cursor = value;
                    OnPropertyChanged();
                }

            }

        }

        #endregion

        #region Methods

        private void SetDefaultBrush()
        {

            if (NotSelectedAndMouseIsNotOverBrush == null)
            {
                NotSelectedAndMouseIsNotOverBrush = CreateBrush("#FF808080");
            }

            if (NotSelectedAndMouseIsOverBrush == null)
            {
                NotSelectedAndMouseIsOverBrush = CreateBrush("#FF646464");
            }

            if (SelectedAndMouseIsNotOverBrush == null)
            {
                SelectedAndMouseIsNotOverBrush = CreateBrush("#FF4747B8");
            }

            if (SelectedAndMouseIsOverBrush == null)
            {
                SelectedAndMouseIsOverBrush = CreateBrush("#FF5757C8");
            }

        }

        private Brush CreateBrush(string color)
        {
            Brush brush = null;

            object convertFromString = ColorConverter.ConvertFromString(color);

            if (convertFromString != null)
            {
                Color colorForBrush = (Color) convertFromString;
                brush = new SolidColorBrush(colorForBrush);
            }

            return brush;
        }

        private void ChangeBackgroundBrush()
        {
            Background = Selected
                ? (IsMouseOver ? selectedAndMouseIsOverBrush : selectedAndMouseIsNotOverBrush)
                : (IsMouseOver ? notSelectedAndMouseIsOverBrush : notSelectedAndMouseIsNotOverBrush);
        }

        public bool Equals(MenuItemStyle other)
        {
            bool result = Selected.Equals(other.Selected) &&
                          RenderTransformOrigin.Equals(other.RenderTransformOrigin) &&
                          Background.Equals(other.Background) &&
                          Cursor.Equals(other.Cursor);

            return result;
        }

        #endregion
    }

}

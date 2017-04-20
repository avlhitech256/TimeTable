namespace CommonControl.TextBlock
{
    public class TransparentTextBlock : System.Windows.Controls.TextBlock
    {
        public TransparentTextBlock()
        {
            //this.Style =new Style(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor);
            //this.Style
            //this.BackColor = Color.Transparent;
            //BorderStyle = System.Windows.Forms.BorderStyle.None;
            Background.Opacity = 0;
        }

    }
}

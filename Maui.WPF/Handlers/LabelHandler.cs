using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Microsoft.Maui.Handlers.WPF
{
    public partial class LabelHandler : WPFViewHandler<ILabel, TextBlock>
    {

        public static IPropertyMapper<ILabel, LabelHandler> Mapper = new PropertyMapper<ILabel, LabelHandler>(ViewHandler.ViewMapper)
        {
            //[nameof(ILabel.Background)] = MapBackground,
            //[nameof(ILabel.Height)] = MapHeight,
            //[nameof(ILabel.Opacity)] = MapOpacity,
            //[nameof(ITextStyle.CharacterSpacing)] = MapCharacterSpacing,
            //[nameof(ITextStyle.Font)] = MapFont,
            //[nameof(ITextAlignment.HorizontalTextAlignment)] = MapHorizontalTextAlignment,
            //[nameof(ITextAlignment.VerticalTextAlignment)] = MapVerticalTextAlignment,
            //[nameof(ILabel.LineHeight)] = MapLineHeight,
            //[nameof(ILabel.Padding)] = MapPadding,
            [nameof(ILabel.Text)] = MapText,
            //[nameof(ITextStyle.TextColor)] = MapTextColor,
            //[nameof(ILabel.TextDecorations)] = MapTextDecorations,
        };

        public static CommandMapper<ILabel, ILabelHandler> CommandMapper = new(ViewCommandMapper)
        {
        };

        public LabelHandler() : base(Mapper)
        {
        }

        public LabelHandler(IPropertyMapper? mapper)
            : base(mapper ?? Mapper, CommandMapper)
        {
        }

        public LabelHandler(IPropertyMapper? mapper, CommandMapper? commandMapper)
            : base(mapper ?? Mapper, commandMapper ?? CommandMapper)
        {
        }

        protected override TextBlock CreatePlatformView() => new TextBlock();

        //public override bool NeedsContainer =>
        //	VirtualView?.Background != null ||
        //	(VirtualView != null && VirtualView.VerticalTextAlignment != TextAlignment.Start) ||
        //	base.NeedsContainer;

        //protected override void SetupContainer()
        //{
        //	base.SetupContainer();

        //	// VerticalAlignment only works when the child's Height is Auto
        //	PlatformView.Height = double.NaN;

        //	MapHeight(this, VirtualView);
        //}

        //protected override void RemoveContainer()
        //{
        //	base.RemoveContainer();

        //	MapHeight(this, VirtualView);
        //}

        //public static void MapHeight(ILabelHandler handler, ILabel view) =>
        //	// VerticalAlignment only works when the container's Height is set and the child's Height is Auto. The child's Height
        //	// is set to Auto when the container is introduced
        //	handler.ToPlatform().UpdateHeight(view);

        //public static void MapBackground(ILabelHandler handler, ILabel label)
        //{
        //	handler.UpdateValue(nameof(IViewHandler.ContainerView));

        //	handler.ToPlatform().UpdateBackground(label);
        //}

        //public static void MapOpacity(ILabelHandler handler, ILabel label)
        //{
        //	handler.UpdateValue(nameof(IViewHandler.ContainerView));
        //	handler.PlatformView.UpdateOpacity(label);
        //	handler.ToPlatform().UpdateOpacity(label);
        //}

        public static void MapText(LabelHandler handler, ILabel label) =>
            handler.PlatformView.Text = label.Text;

        //public static void MapTextColor(ILabelHandler handler, ILabel label) =>
        //	handler.PlatformView?.UpdateTextColor(label);

        //public static void MapCharacterSpacing(ILabelHandler handler, ILabel label) =>
        //	handler.PlatformView?.UpdateCharacterSpacing(label);

        //public static void MapFont(ILabelHandler handler, ILabel label)
        //{
        //	var fontManager = handler.GetRequiredService<IFontManager>();

        //	handler.PlatformView?.UpdateFont(label, fontManager);
        //}

        //public static void MapHorizontalTextAlignment(ILabelHandler handler, ILabel label) =>
        //	handler.PlatformView?.UpdateHorizontalTextAlignment(label);

        //public static void MapVerticalTextAlignment(ILabelHandler handler, ILabel label)
        //{
        //	handler.UpdateValue(nameof(IViewHandler.ContainerView));

        //	handler.PlatformView?.UpdateVerticalTextAlignment(label);
        //}

        //public static void MapTextDecorations(ILabelHandler handler, ILabel label) =>
        //	handler.PlatformView?.UpdateTextDecorations(label);

        //public static void MapPadding(ILabelHandler handler, ILabel label) =>
        //	handler.PlatformView?.UpdatePadding(label);

        //public static void MapLineHeight(ILabelHandler handler, ILabel label) =>
        //	handler.PlatformView?.UpdateLineHeight(label);
    }
}

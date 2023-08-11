using System;
using System.Windows;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Platform;
using Microsoft.Maui.Platform.WPF;

namespace Microsoft.Maui.Handlers.WPF
{

    public partial class LayoutHandler : WPFViewHandler<Layout, LayoutPanel>
    {
        public static IPropertyMapper<Layout, LayoutHandler> Mapper = new PropertyMapper<Layout, LayoutHandler>(ViewMapper)
        {
            [nameof(ILayout.Background)] = MapBackground,
            [nameof(ILayout.ClipsToBounds)] = MapClipsToBounds,
#if ANDROID || WINDOWS
            [nameof(IView.InputTransparent)] = MapInputTransparent,
#endif
        };

        public static CommandMapper<Layout, LayoutHandler> CommandMapper = new(ViewCommandMapper)
        {
            [nameof(ILayoutHandler.Add)] = MapAdd,
            [nameof(ILayoutHandler.Remove)] = MapRemove,
            [nameof(ILayoutHandler.Clear)] = MapClear,
            [nameof(ILayoutHandler.Insert)] = MapInsert,
            [nameof(ILayoutHandler.Update)] = MapUpdate,
            [nameof(ILayoutHandler.UpdateZIndex)] = MapUpdateZIndex,
        };

        public LayoutHandler() : base(Mapper, CommandMapper)
        {
        }

        public LayoutHandler(IPropertyMapper? mapper = null, CommandMapper? commandMapper = null)
            : base(mapper ?? Mapper, commandMapper ?? CommandMapper)
        {

        }

        public static void MapBackground(LayoutHandler handler, ILayout layout)
        {
        }

        public static void MapClipsToBounds(LayoutHandler handler, ILayout layout)
        {
        }

        public static void MapAdd(LayoutHandler handler, ILayout layout, object? arg)
        {
            if (arg is LayoutHandlerUpdate args)
            {
                handler.Add(args.View);
            }
        }

        public static void MapRemove(LayoutHandler handler, ILayout layout, object? arg)
        {
            if (arg is LayoutHandlerUpdate args)
            {
                handler.Remove(args.View);
            }
        }

        public static void MapInsert(LayoutHandler handler, ILayout layout, object? arg)
        {
            if (arg is LayoutHandlerUpdate args)
            {
                handler.Insert(args.Index, args.View);
            }
        }

        public static void MapClear(LayoutHandler handler, ILayout layout, object? arg)
        {
            handler.Clear();
        }

        static void MapUpdate(LayoutHandler handler, ILayout layout, object? arg)
        {
            if (arg is LayoutHandlerUpdate args)
            {
                handler.Update(args.Index, args.View);
            }
        }

        static void MapUpdateZIndex(LayoutHandler handler, ILayout layout, object? arg)
        {
            if (arg is IView view)
            {
                handler.UpdateZIndex(view);
            }
        }

        /// <summary>
        /// Converts a FlowDirection to the appropriate FlowDirection for cross-platform layout 
        /// </summary>
        /// <param name="flowDirection"></param>
        /// <returns>The FlowDirection to assume for cross-platform layout</returns>
        internal static FlowDirection GetLayoutFlowDirection(FlowDirection flowDirection)
        {
#if WINDOWS
            // The native LayoutPanel in Windows will automagically flip our layout coordinates if it's in RTL mode.
            // So for cross-platform layout purposes, we just always treat things as being LTR and let the Panel sort out the rest.
            return FlowDirection.LeftToRight;
#else
			return flowDirection;
#endif
        }

        public void Add(IView child)
        {
            _ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
            _ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
            _ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

            var targetIndex = VirtualView.IndexOf(child);
            PlatformView.Children.Insert(targetIndex, (UIElement)child.ToPlatform(MauiContext));
        }

        public override void SetVirtualView(IView view)
        {
            base.SetVirtualView(view);

            _ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
            _ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
            _ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

            PlatformView.CrossPlatformMeasure = VirtualView.CrossPlatformMeasure;
            PlatformView.CrossPlatformArrange = VirtualView.CrossPlatformArrange;

            PlatformView.Children.Clear();

            foreach (var child in VirtualView)
            {
                PlatformView.Children.Add((UIElement)child.ToPlatform(MauiContext));
            }
        }

        public void Remove(IView child)
        {
            _ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
            _ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");

            if ((child.Handler?.ContainerView ?? child.Handler?.PlatformView) is UIElement view)
            {
                PlatformView.Children.Remove(view);
            }
        }

        public void Clear()
        {
            PlatformView?.Children.Clear();
        }

        public void Insert(int index, IView child)
        {
            _ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
            _ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
            _ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

            var targetIndex = VirtualView.IndexOf(child);
            PlatformView.Children.Insert(targetIndex, (UIElement)child.ToPlatform(MauiContext));
        }

        public void Update(int index, IView child)
        {
            _ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
            _ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
            _ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

            PlatformView.Children[index] = (UIElement)child.ToPlatform(MauiContext);
            EnsureZIndexOrder(child);
        }

        public void UpdateZIndex(IView child)
        {
            _ = PlatformView ?? throw new InvalidOperationException($"{nameof(PlatformView)} should have been set by base class.");
            _ = VirtualView ?? throw new InvalidOperationException($"{nameof(VirtualView)} should have been set by base class.");
            _ = MauiContext ?? throw new InvalidOperationException($"{nameof(MauiContext)} should have been set by base class.");

            EnsureZIndexOrder(child);
        }

        protected override LayoutPanel CreatePlatformView()
        {
            if (VirtualView == null)
            {
                throw new InvalidOperationException($"{nameof(VirtualView)} must be set to create a LayoutViewGroup");
            }

            var view = new LayoutPanel
            {
                CrossPlatformMeasure = VirtualView.CrossPlatformMeasure,
                CrossPlatformArrange = VirtualView.CrossPlatformArrange,
            };

            return view;
        }

        protected override void DisconnectHandler(LayoutPanel platformView)
        {
            // If we're being disconnected from the xplat element, then we should no longer be managing its children
            platformView.Children.Clear();
            base.DisconnectHandler(platformView);
        }

        void EnsureZIndexOrder(IView child)
        {
            if (PlatformView.Children.Count == 0)
            {
                return;
            }

            var currentIndex = PlatformView.Children.IndexOf((UIElement)child.ToPlatform(MauiContext!));

            if (currentIndex == -1)
            {
                return;
            }

            //var targetIndex = VirtualView.IndexOf(child);

            //if (currentIndex != targetIndex)
            //{
            //	PlatformView.Children.Move((uint)currentIndex, (uint)targetIndex);
            //}
        }

        static void MapInputTransparent(ILayoutHandler handler, ILayout layout)
        {
            //if (handler.PlatformView is LayoutPanel layoutPanel && layout != null)
            //{
            //	layoutPanel.UpdatePlatformViewBackground(layout);
            //}
        }
    }
}
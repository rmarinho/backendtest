namespace Microsoft.Maui.Handlers.WPF
{
    class WPFActivationState : ActivationState
    {
        public WPFActivationState(IMauiContext context) : base(context)
        {
        }

        public WPFActivationState(IMauiContext context, IPersistedState state) : base(context, state)
        {
        }

        public WPFPersistedState? WPFPersistedState => base.State as WPFPersistedState;
    }
}

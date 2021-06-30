using System;

namespace MykaelosUnityLibrary.DataBind {

    /**
     * A simple property wrapper that calls the ValueChanged event when updated.
     * 
     * Partially borrowed from http://unity-coding.slashgames.org/the-magic-behind-data-binding-part-1/ with some tweaks.
     */
    public class DataBindProperty<T> {
        private T hiddenValue;

        public T Value {
            get { return hiddenValue; }
            set {
                if (Equals(hiddenValue, value)) {
                    return;
                }

                hiddenValue = value;
                OnValueChanged();
            }
        }


        public DataBindProperty() { }

        public DataBindProperty(T value) {
            Value = value; // Triggers OnValueChanged().
        }

        public event Action ValueChanged;

        protected void OnValueChanged() {
            ValueChanged?.Invoke();
        }
    }
}

/* Example:
 * 
 * DataBindProperty<int> Power = new DataBindProperty<int>();
 * 
 * private void Setup(Power power) {
 *     Power = power;
 * 
 *     Power.ValueChanged += OnPowerChanged; // Add callback.
 * }
 * 
 * private void OnDestroy() {
 *     Power.ValueChanged -= OnPowerChanged; // Remove to prevent lingering references.
 * }
 * 
 * private void OnPowerChanged() {
 *     TextBox.text = Power.Value.ToString(); // Do something with the updated value.
 * }
 */

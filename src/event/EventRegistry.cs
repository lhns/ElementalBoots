using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolHens
{
    public class EventRegistry
    {
        private List<EventListener> listeners = new List<EventListener>();

        public void Register<E>(Action<E> listener, Object monitor = null) where E : Event
        {
            listeners.Add(new EventListener(e => listener((E)e), typeof(E), monitor));
        }

        public Boolean Call<E>(E lolHensEvent) where E : Event
        {
            List<EventListener> listenersCopy = new List<EventListener>();
            listenersCopy.AddRange(listeners);

            if (lolHensEvent.CallOnce()) Remove<E>();

            lolHensEvent.OnEventPre();

            foreach (EventListener listener in listenersCopy)
            {
                if (listener.action == null)
                {
                    listeners.Remove(listener);
                }
                else if (listener.type == typeof(E))
                {
                    listener.action(lolHensEvent);
                    if (lolHensEvent.Cancelled()) break;
                }
            }

            lolHensEvent.OnEventPost();

            return !lolHensEvent.Cancelled();
        }

        public void Remove<E>() where E : Event
        {
            listeners.RemoveAll(listener => listener.type == typeof(E));
        }
    }

    class EventListener
    {
        public readonly Action<Event> action;
        public readonly Type type;
        private WeakReference monitor;

        public EventListener(Action<Event> action, Type type, Object monitor)
        {
            this.action = action;
            this.type = type;
            monitor = monitor == null ? null : new WeakReference(monitor);
        }

        public bool IsAlive
        {
            get { return monitor == null || monitor.IsAlive; }
        }
    }
}

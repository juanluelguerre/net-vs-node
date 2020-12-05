//
// https://docs.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh229114(v=vs.103)
//
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RxConsole
{
	// The Ticket class just represents a hypothetical ticket composed of an ID and a timestamp.
	class Ticket
    {
        private readonly string _ticketId;
        private readonly DateTime _timeStamp;

        public Ticket(string tid)
        {
            _ticketId = tid;
            _timeStamp = DateTime.Now;
        }

        public override string ToString()
        {
            return String.Format("Ticket ID : {0}\nTimestamp : {1}\n", _ticketId, _timeStamp.ToString());
        }
    }

    /// <summary>
    /// The TicketFactory class generates a new sequence of tickets on a separate thread. The
    /// generation of the sequence is initiated by the TicketSubscribe method which will be passed
    /// to Observable.Create().    
    /// TicketSubscribe() provides the IDisposable interface used to dispose of the subscription
    /// stopping ticket generation resources.
    /// </summary>
    public class TicketFactory : IDisposable
    {
        private bool _generate = true;

        internal TicketFactory(object ticketObserver)
        {            
            // The sequence generator for tickets will be run on another thread         
            Task.Factory.StartNew(new Action<object>(TicketGenerator), ticketObserver);
        }
        
        // Dispose frees the ticket generating resources by allowing the TicketGenerator to complete.        
        public void Dispose()
        {
            _generate = false;
        }
        
        // TicketGenerator generates a new ticket every 3 sec and calls the observer's OnNext handler to deliver it.        
        private void TicketGenerator(object observer)
        {
            IObserver<Ticket> ticketObserver = (IObserver<Ticket>)observer;

          
            // Generate a new ticket every 3 sec and call the observer's OnNext handler to deliver it.          
            Ticket t;

            while (_generate)
            {
                t = new Ticket(Guid.NewGuid().ToString());
                ticketObserver.OnNext(t);
                Thread.Sleep(3000);
            }
        }

        /// <summary>
        ///  TicketSubscribe starts the flow of tickets for the ticket sequence when a subscription is created. It is passed to
        ///  Observable.Create() as the subscribe parameter. Observable.Create() returns the IObservable<Ticket> that is used to 
        ///  create subscriptions by calling the IObservable<Ticket>.Subscribe() method.
        ///  The IDisposable interface returned by TicketSubscribe is returned from the IObservable<Ticket>.Subscribe() call. Calling
        ///  Dispose cancels the subscription freeing ticket generating resources.  
        /// </summary>
        /// <param name="ticketObserver"></param>
        /// <returns></returns>
        public static IDisposable TicketSubscribe(object ticketObserver)
        {
            var tf = new TicketFactory(ticketObserver);

            return tf;
        }
    }
}

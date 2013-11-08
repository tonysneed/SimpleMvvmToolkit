SimpleMvvm-Async Sample

This sample shows how to use the Simple Mvvm Toolkit to perform asynchronous
operations. This can be helpful if you are in a for loop and need to suspend
the loop in order to prompt the user.  Because dialogs in Silverlight are
by nature asynchronous, you will not be able to halt the loop if you are running
it on the main UI thread.

The solution is to run the loop in code that runs on a worker thread, using
a synchronization device like AutoResetEvent to block the looping thread until
a reponse is received back.  The difficuly here is that the dialog can only
be shown on the UI thread, so you would have to muck around with Dispatcher
.BeginInvoke from your code.

The ViewModelBase class, however, has a Notify method you can call to fire
a notification event that is handled by the view to display a dialog.  This
method will check to see if it's on the UI thread and marshal the call over
if it's not.

NOTE: You should NEVER display dialogs directly from a view-model because
it violates separation of concerns and makes the view-model untestable.
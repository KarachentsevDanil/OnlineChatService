import * as signalR from '@aspnet/signalr'

export default {
  install (Vue) {
    // use a new Vue instance as the interface for Vue components to receive/send SignalR events
    // this way every component can listen to events or send new events using this.$chatHub
    const chatHub = new Vue()
    Vue.prototype.$chatHub = chatHub

    // Provide methods to connect/disconnect from the SignalR hub
    let connection = null
    let startedPromise = null
    let manuallyClosed = false
    let isOpened = false

    Vue.prototype.startSignalR = async (jwtToken) => {
      console.log("invoked");
      if(isOpened){
        return;
      }

      connection = new signalR.HubConnectionBuilder()
        .withUrl(
          'https://dkarachatapi.azurewebsites.net/signalr/chats/',
          { accessTokenFactory: () => jwtToken }
        )
        .build()

      // Forward hub events through the event, so we can listen for them in the Vue components
      connection.on('ChatMessageHub', (message) => {
        chatHub.$emit('message-added', message)
      })

      // You need to call connection.start() to establish the connection but the client wont handle reconnecting for you!
      // Docs recommend listening onclose and handling it there.
      // This is the simplest of the strategies
      async function start() {
        try {
            console.log("started");
            await connection.start();
            console.log("connected", connection.state);
        } catch (err) {
            console.log(err);
        }
      };

      // Start everything
      manuallyClosed = false
      isOpened = true
      setTimeout(async ()=>{
        await start()
      }, 1000);
    };

    Vue.prototype.stopSignalR = () => {
      if (!startedPromise) return

      // manuallyClosed = true
      // return startedPromise
      //   .then(() => connection.stop())
      //   .then(() => { startedPromise = null })
    };

    // Provide methods for components to send messages back to server
    // Make sure no invocation happens until the connection is established
    chatHub.sendMessage = (message) => {
      if(!isOpened){
        connection.start();
      }

      return connection.invoke('SendPrivateMessageAsync', message);
    }
  }
}
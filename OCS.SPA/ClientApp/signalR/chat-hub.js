import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'

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

    Vue.prototype.startSignalR = (jwtToken) => {
      connection = new HubConnectionBuilder()
        .withUrl(
          'https://localhost:5005/signalr/chats/',
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
      function start () {
        startedPromise = connection.start()
          .catch(err => {
            console.error('Failed to connect with hub', err);
          }).then(function () {
            console.log("connected");
          });

        return startedPromise
      }
      connection.onclose(() => {
        if (!manuallyClosed) start()
      })

      // Start everything
      manuallyClosed = false
      start()
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
      if (!startedPromise) return

      return startedPromise
        .then(() => connection.invoke('SendPrivateMessageAsync', message))
        .catch(console.error)
    }
  }
}
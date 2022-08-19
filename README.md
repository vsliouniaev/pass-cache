Replaced with https://github.com/vsliouniaev/go-pass-cache

Use this tool instead of sending passwords directly through your instant messaging client. 

The data is encrypted in your browser, then sent to the server and kept for 5 minutes in the server's RAM.
You then send the id and the decrytpion key over IM, keeping the data and the key through separate channels.
Once the client retrieves the data, it is deleted from the server and decrypted in the browser.

This tool adds very little friction while greatly increasing the security. Security is always a balance between ease-of-use and provided security. While not being particularly secure, the ease of use greatly improves overall security. I would not advise this approach for sending anything you _truly_ want to keep safe, but it it is a great first step if you are currently sending this directly through Skype or Slack.

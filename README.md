# DiscordBot

A Discord bot that can be used to test semantic kernel https://github.com/microsoft/semantic-kernel

## Setup

1. Create a Discord bot and get the token

2. Go to in solution folder and run
``` 
git submodule init
git submodule update
```

3. Add secrets to DiscordBot 

```
{
  "Bot:Key": "YOUR-DISCORD-BOT-KEY",
  "OPENAI_API_KEY": "OPENAI_API_KEY"
  "Completion:Key": "OPENAI_API_KEY",
  "TextCompletion:Key": "OPENAI_API_KEY",
  "Embedding:Key": "OPENAI_API_KEY",
  "BING_API_KEY": "BING_API_KEY",
  "Twitter:ConsumerKey": "CONSUMER_KEY",
  "Twitter:ConsumerSecret ": "CONSUMER_SECRETS",
  "Twitter:AccessKey ": "API_TWITTER",
  "Twitter:AccessSecret ": "API_TWITTER"
  }
}
```

## Commands

### /ping

get pong

### /ask [goal]

perform a goal with ActionPlanner

### /plan [goal]

perform a goal with SequentialPlanner

### /twitter-auth

open a new web page to authenticate with Twitter and get pin to use in /twitter-pin

### /twitter-pin [pin]

authenticate with Twitter using pin from /twitter-auth
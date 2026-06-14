Simple Window based program to evaluate prices for items in video game Warframe using warframe.market api to get relevant data.

To use application put your item data into data.json in Data directory, format:
[
{ "name": "{item name to lower case, space is replaced with "_", add "_set" for sets}", "amount": {number} },
...
]

example for data.json:
[
{ "name": "braton_prime_set", "amount": 1 },
{ "name": "khora_prime_blueprint", "amount": 2 }
]

This tool does not guarantee prices accuracy regarding item prices - use it to get a general idea about items' prices

This project is not affiliated with Digital Extremes or Warframe Market

# MarketFlight

## FLIGTHS

### marketflight/fligths

GET - Get all flights
POST - Create a flight

### marketflight/fligths/:fligth_id

GET - Get one flight
PUT - Update one flight //later
DELETE - Delete one flight

## USERS

### marketflight/users

GET - Get all users
POST - Create a user  params: firstName(string), lastName(string)

### marketflight/users/:user_id

GET - Get a user
PUT - Update a user //Later
DELETE - Delete a user //later, hard(what do we do if the user have orders ?)

### marketflight/users/:user_id/orders

GET - Get all orders for a given user

## ORDERS

### marketflight/orders

GET - Get all orders
POST - Create an order //return -1 if not enough seat

### marketflight/orders/:order_id

GET - Get an order
PUT - Update an order //Later
DELETE - Delete an order //Later

### market

# MarketFlight

## FLIGTHS

### marketflight/fligths

GET - Get all flights
POST - Create a flight

### marketflight/fligths/:fligth_id

GET - Get one flight
PUT - Update one flight
DELETE - Delete one flight

## USERS

### marketflight/users

GET - Get all users  params: userId(number)
POST - Create an user params: firstName(string), lastName(string)

### marketflight/users/:user_id

GET - Get an user
PUT - Update an user
DELETE - Delete an user

### marketflight/users/:user_id/flights

GET - Get all flights for an given user

## ORDERS

### marketflight/orders

GET - Get all orders
POST - Create an order

### marketflight/orders/:order_id

GET - Get an order
PUT - Update an order
DELETE - Delete an order

<script lang="ts">
    // Imports
    import type { FlightMode } from '../../Utils';
    import { Button } from 'sveltestrap';
    import Icon from 'svelte-awesome/components/Icon.svelte';
    import { faExclamationTriangle } from '@fortawesome/free-solid-svg-icons';
    import { Flight } from '../../Models/Flight';
    import { User } from '../../Models/User';
    import UserApiRepo from '../../Repository/UserApiRepo';
    import FlightApiRepo from '../../Repository/FlightApiRepo';
    import OrdersApiRepo from '../../Repository/OrdersApiRepo';
    import { Order } from '../../Models';

    export let choosenFlightMode: FlightMode = 'oneWay';
    export let flights: Flight[] = [];
    export let formVisible = false;

    export let choosenFlight: Flight = new Flight();
    export let formNewUser: User = new User();
    export let userService = new UserApiRepo();
    export let flightService = new FlightApiRepo();
    export let ordersService = new OrdersApiRepo();

    function chooseFlightMode(mode: FlightMode) {
        choosenFlightMode = mode;
    }

    function chooseFlight(flight: Flight) {
        formVisible = true;
        choosenFlight = flight;
    }

    async function bookFlight() {
        console.log(choosenFlight);
        console.log(formNewUser);

        const user = await userService.CreateUser(formNewUser);

        const newOrder = new Order();
        newOrder.FlightId = choosenFlight.FlightId;
        newOrder.OrderDate = new Date();
        newOrder.UserId = user.UserId;
        newOrder.DepartureAirportName = choosenFlight.DepartureAirportName;
        newOrder.ArrivalAirportName = choosenFlight.ArrivalAirportName;

        await ordersService.CreateOrder(newOrder);
        // WIP Create New User
        // WIP Create Order
    }

    flightService.GetAllFlights().then((data) => {
        console.log(data);
    });
</script>

<style>
    main {
        text-align: center;
        padding: 1em;
        max-width: 240px;
        margin: 0 auto;
    }

    .warning-block {
        padding: 18px;
        width: auto;
        max-width: 30%;
        min-height: 50px;
        background-color: #ff7f7f;
    }

    .warning-text {
        flex: 1;
        margin-bottom: 0;
    }

    .icon-container {
        flex: 0.1;
    }

    .table-container,
    .form-container {
        width: 46%;
        margin-left: auto;
        margin-right: auto;
    }

    table {
        border-collapse: collapse;
        width: 100%;
    }

    th,
    td {
        padding: 8px;
        text-align: center;
        border-bottom: 1px solid #ddd;
    }

    input[type='text'] {
        width: 100%;
        padding: 12px 20px;
        margin: 8px 0;
        display: inline-block;
        border: 1px solid #ccc;
        border-radius: 4px;
        box-sizing: border-box;
    }

    @media (min-width: 640px) {
        main {
            max-width: none;
        }
    }
</style>

<main>
    <h1 class="my-4">Réservez des vols sans attendre !</h1>

    <div class="warning-block ml-auto mr-auto d-flex flex-center">
        <div class="icon-container">
            <Icon class="mr-2" data={faExclamationTriangle} />
        </div>
        <p class="warning-text">
            Il est désormais obligatoire de porter un masque chirurgical dès votre arrivée à l’aéroport et pendant toute
            la durée des vols !
        </p>
    </div>
    <div class="radio-buttons my-4">
        <Button
            class="btn btn-light {choosenFlightMode === 'oneWay' ? 'button-active' : ''}"
            on:click={() => chooseFlightMode('oneWay')}>
            Aller Simple
        </Button>
        <Button
            class="btn btn-light {choosenFlightMode === 'roundTrip' ? 'button-active' : ''}"
            on:click={() => chooseFlightMode('roundTrip')}>
            Aller Retour
        </Button>
    </div>

    <div class="table-container mt-6 mb-6">
        <table>
            <tr>
                <th>Departure</th>
                <th>Arrival</th>
                <th>Price (€)</th>
            </tr>
            {#each flights as flight}
                <tr>
                    <td>{flight.DepartureAirportName}</td>
                    <td>{flight.ArrivalAirportName}</td>
                    <td><button on:click={() => chooseFlight(flight)}> Book this flight ! </button></td>
                </tr>
            {/each}
        </table>
    </div>

    <div class="form-container mt-6 {formVisible ? '' : 'hidden'}">
        <form>
            <label for="firstName">First Name</label>
            <input name="firstName" type="text" bind:value={formNewUser.FirstName} />
            <label for="lastName">Last Name</label>
            <input name="lastName" type="text" bind:value={formNewUser.LastName} />

            <button on:click={() => bookFlight()}> Book ! </button>
        </form>
    </div>
</main>

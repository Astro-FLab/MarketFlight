<script lang="ts">
    // Imports
    import type { FlightMode } from '../../Utils';
    import { Button } from 'sveltestrap';
    import Icon from 'svelte-awesome/components/Icon.svelte';
    import { faExclamationTriangle } from '@fortawesome/free-solid-svg-icons';
    import { Flight } from '../../Models/Flight';
    import { User } from '../../Models/User';
    import UserApiRepo from '../../Repository/UserApiRepo';
    import OrdersApiRepo from '../../Repository/OrdersApiRepo';
    import AirportsApiRepo from '../../Repository/AirportsApiRepo';
    import { Bundle, Order } from '../../Models';
    import { onMount } from 'svelte';
    import { CurrentUserService } from '../../Helpers/CurrentUserService';
    import BundleApiRepo from '../../Repository/BundleApiRepo';

    export let choosenFlightMode: FlightMode = 'oneWay';
    export let bundles: Bundle[] = [];
    export let formVisible = false;

    export let choosenBundle: Bundle = new Bundle();
    export let formNewUser: User = new User();
    export let userService = new UserApiRepo();
    export let ordersService = new OrdersApiRepo();
    export let airportService = new AirportsApiRepo();
    export let bundleService = new BundleApiRepo();
    export let currentUserService: CurrentUserService;

    function chooseFlightMode(mode: FlightMode) {
        choosenFlightMode = mode;
    }

    function chooseBundle(bundle: Bundle) {
        formVisible = true;
        choosenBundle = bundle;
    }

    function computePrice(bundle: Bundle): string {
        let price: number;
        if (bundle.flights.length > 1) {
            price = bundle.price - (10 / 100) * bundle.price;
            return `${price} (-10%)`;
        } else {
            return price.toString();
        }
    }

    async function bookFlight(event: MouseEvent) {
        event.preventDefault();
        const userId = await userService.CreateUserIfNotExist(formNewUser);
        currentUserService.currentUserId = userId;

        const departureAirport = await airportService.GetAirportByName(choosenBundle.flights[0].departureAirportName);

        for (let i = 0; i < choosenBundle.flights.length; i++) {
            const element = choosenBundle.flights[i];
            const newOrder = new Order();
            newOrder.flightId = element.flightId;
            newOrder.orderDate = new Date();
            newOrder.userId = userId;
            newOrder.departureAirportId = departureAirport.airportId;
            newOrder.departureAirportName = departureAirport.name;
            newOrder.arrivalAirportName = element.arrivalAirportName;

            await ordersService.CreateOrder(newOrder);
        }
    }

    onMount(async () => {
        currentUserService = CurrentUserService.getInstance();
        bundles = await bundleService.GetAll();
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
    <h1 class="my-4">Book flights now !</h1>

    <div class="warning-block ml-auto mr-auto d-flex flex-center">
        <div class="icon-container">
            <Icon class="mr-2" data={faExclamationTriangle} />
        </div>
        <p class="warning-text">
            It is now mandatory to wear a surgical mask upon arrival at the airport and for the entire duration of the
            flights !
        </p>
    </div>
    <div class="radio-buttons my-4">
        <Button
            class="btn btn-light {choosenFlightMode === 'oneWay' ? 'button-active' : ''}"
            on:click={() => chooseFlightMode('oneWay')}>
            One way
        </Button>
        <Button
            class="btn btn-light {choosenFlightMode === 'roundTrip' ? 'button-active' : ''}"
            on:click={() => chooseFlightMode('roundTrip')}>
            Return
        </Button>
    </div>

    <div class="table-container mt-6 mb-6">
        <table>
            <tr>
                <th>Departure</th>
                <th>Arrival</th>
                <th>Price (€)</th>
            </tr>
            {#each bundles as bundle}
                <tr>
                    <td>{bundle.flights[0].departureAirportName}</td>
                    <td>{bundle.flights[bundle.flights.length - 1].arrivalAirportName}</td>
                    <td><button on:click={() => chooseBundle(bundle)}> {computePrice(bundle)} </button></td>
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
            <label for="luggages">Luggages</label>
            <input name="luggages" type="number" />

            <button on:click={(e) => bookFlight(e)}> Confirm ! </button>
        </form>
    </div>
</main>

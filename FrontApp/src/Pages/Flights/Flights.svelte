<script lang="ts">
    // Imports
    import type { FlightMode } from '../../Utils';
    import { Button } from 'sveltestrap';
    import Icon from 'svelte-awesome/components/Icon.svelte';
    import { faExclamationTriangle } from '@fortawesome/free-solid-svg-icons';
    import type { Flight } from '../../Models/Flight';
    import type { User } from '../../Models/User';

    export let choosenFlightMode: FlightMode = 'oneWay';
    export let flights: Flight[] = [];
    export let formNewUser: User = null;
    export let formVisible = false;

    function chooseFlightMode(mode: FlightMode) {
        choosenFlightMode = mode;
    }

    function chooseFlight(flight: Flight) {
        formVisible = true;
    }
    console.log('GOOO =>');
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

    .table-container {
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

    <div class="table-container mt-6">
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
                    <td><button on:click> Book this flight ! </button></td>
                </tr>
            {/each}
        </table>
    </div>
</main>

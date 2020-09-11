<script lang="ts">
    // Imports
    import { Button } from 'sveltestrap';
    import FlightApiRepo from '../../../Repository/FlightApiRepo';
    import AirportApiRepo from '../../../Repository/AirportsApiRepo';
    import { Flight } from '../../../Models';
    import { onMount } from 'svelte';

    export let flights: Flight[] = [];

    export let flightService = new FlightApiRepo();
    export let airportService = new AirportApiRepo();

    export let formNewFlight: Flight = new Flight();

    async function addFlight(e) {
        e.preventDefault();
        const airportArrival = await airportService.GetAirportByName(formNewFlight.arrivalAirportName);
        const airportDeparture = await airportService.GetAirportByName(formNewFlight.departureAirportName);
        console.log(airportArrival[0].airportId);
        console.log(airportDeparture[0]);

        formNewFlight.arrivalAirportId = airportArrival[0].airportId;
        formNewFlight.departureAirportId = airportDeparture[0].airportId;
        await flightService.CreateFlight(formNewFlight);
    }

    onMount(async () => {
        flights = await flightService.GetAllFlights();
    });
</script>

<style>
    main {
        text-align: center;
        padding: 1em;
        max-width: 240px;
        margin: 0 auto;
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
    <h1 class="my-4">Flights List</h1>

    <div class="table-container mt-6 mb-6">
        <table>
            <tr>
                <th>Departure</th>
                <th>Arrival</th>
                <th>Price (€)</th>
            </tr>
            {#each flights as flight}
                <tr>
                    <td>{flight.departureAirportName}</td>
                    <td>{flight.arrivalAirportName}</td>
                    <td>?</td>
                </tr>
            {/each}
        </table>
    </div>

    <h1 class="my-1">Add a flight</h1>

    <div class="form-container mt-6">
        <form>
            <label for="departure">Flight Departure</label>
            <input name="departure" type="text" bind:value={formNewFlight.departureAirportName} />

            <label for="arrival">Flight Arrival</label>
            <input name="arrival" type="text" bind:value={formNewFlight.arrivalAirportName} />

            <button on:click={(e) => addFlight(e)}> Add it ! </button>
        </form>
    </div>
</main>

<script lang="ts">
    // Imports
    import MultiSelect from '../../../Components/MultiSelect.svelte';
    import { Button } from 'sveltestrap';
    import BundleApiRepo from '../../../Repository/BundleApiRepo';
    import FlightApiRepo from '../../../Repository/FlightApiRepo';
    import { Bundle, Flight } from '../../../Models';
    import { onMount } from 'svelte';
    import Flights from '../../Flights/Flights.svelte';

    export let bundles: Bundle[] = [];

    export let bundleService = new BundleApiRepo();

    export let formNewBundle: Bundle = new Bundle();

    export let formVisible = false;
    export let choosenBundle: Bundle = new Bundle();

    export let flights: Flight[] = [];
    export let flightService = new FlightApiRepo();
    export let choosenFlight: Flight[] = [];

    async function addBundle(e) {
        e.preventDefault();
        bundleService.CreateBundle(formNewBundle.price);
    }

    function setBundle(bundle) {
        formVisible = true;
        choosenBundle = bundle;
    }

    function addItemToBundle(e) {
        e.preventDefault();
        choosenFlight.forEach((element) => {
            bundleService.AddItem(choosenBundle.bundleId, element.flightId);
        });
    }

    onMount(async () => {
        flights = await flightService.GetAllFlights();
        //bundles = await bundleService.GetAllBundle();
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
    <h1 class="my-4">Bundles List</h1>

    <div class="table-container mt-6 mb-6">
        <table>
            <tr>
                <th>Bundle n°</th>
                <th>Price (€)</th>
                <th>Choice</th>
            </tr>
            {#each bundles as bundle}
                <tr>
                    <td>{bundle.bundleId}</td>
                    <td>{bundle.price}</td>
                    <td><button on:click={() => setBundle(bundle)}> Set this bundle </button></td>
                </tr>
            {/each}
        </table>
    </div>

    <div class="form-container mt-6 {formVisible ? '' : 'hidden'}">
        <form>
            <h3>Bundle n° {formNewBundle.bundleId}</h3>
            <label for="lastName">Bundle Price</label>
            <input name="lastName" type="text" bind:value={formNewBundle.price} />
            <label for="flights">Flight</label>

            <MultiSelect bind:choosenFlight>
                {#each flights as flight}
                    <option value={flight}>{flight.departureAirportName} - {flight.arrivalAirportName}</option>
                {/each}
            </MultiSelect>
            <button on:click={(e) => addItemToBundle(e)}> Add Item ! </button>
        </form>
    </div>

    <h1 class="my-1">Add a Bundle</h1>

    <div class="form-container mt-6">
        <form>
            <label for="arrival">Bundle Price</label>
            <input name="arrival" type="text" bind:value={formNewBundle.price} />

            <button on:click={(e) => addBundle(e)}> Add it ! </button>
        </form>
    </div>
</main>

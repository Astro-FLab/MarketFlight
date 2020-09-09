<script lang="ts">
    // Imports
    import { Button } from 'sveltestrap';
    import AirportsApiRepo from '../../Repository/AirportsApiRepo';
    import { Airport } from '../../Models';
    import { onMount } from 'svelte';

    export let airports: Airport[] = [];

    export let airportService = new AirportsApiRepo();

    export let formNewAirport: Airport = new Airport();

    async function addAirport(e) {
        e.preventDefault();
        await airportService.CreateAirport(formNewAirport);
    }

    onMount(async () => {
        airports = await airportService.GetAllAirPorts();
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
    <h1 class="my-4">AirPorts List</h1>

    <div class="table-container mt-6 mb-6">
        <table>
            <tr>
                <th>Airport Name</th>
            </tr>
            {#each airports as airport}
                <tr>
                    <td>{airport.name}</td>
                </tr>
            {/each}
        </table>
    </div>

    <h1 class="my-1">Add an airport</h1>

    <div class="form-container mt-6">
        <form>
            <label for="name">Airport Name</label>
            <input name="name" type="text" bind:value={formNewAirport.name} />

            <button on:click={(e) => addAirport(e)}> Add it ! </button>
        </form>
    </div>
</main>

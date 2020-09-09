<script lang="ts">
    import FlightApiRepo from '../../Repository/FlightApiRepo';
    import type { Flight } from '../../Models';

    let flightService = new FlightApiRepo();

    console.log('GOOO =>');
    // const allFlights = async (): Promise<Flight> => {
    flightService.GetAllFlights().then((data) => {
        console.log(data);
    });
    //return data;
    // };
</script>

<style>
    main {
        text-align: center;
        padding: 1em;
        max-width: 240px;
        margin: 0 auto;
    }

    h1 {
        color: orange;
        text-transform: uppercase;
        font-size: 4em;
        font-weight: 100;
    }

    @media (min-width: 640px) {
        main {
            max-width: none;
        }
    }
</style>

<main>
    <h1>Flights</h1>
</main>

<script lang="ts">
    import { Router, Link, Route } from 'svelte-routing';
    import Flights from './Pages/Flights/Flights.svelte';
    import Icon from 'svelte-awesome/components/Icon.svelte';
    import {
        faHome,
        faMoon,
        faPlane,
        faPlaneArrival,
        faPlaneDeparture,
        faRoute,
    } from '@fortawesome/free-solid-svg-icons';
    import HomePage from './Pages/HomePage/HomePage.svelte';
    import MyOrders from './Pages/MyOrders/MyOrders.svelte';
    import Airports from './Pages/Airports/Airports.svelte';

    export let name: string;
    export let url = '';

    function toggleDarkMode() {
        window.document.body.classList.toggle('dark-mode');
    }
</script>

<style>
    :global(body.dark-mode) {
        background-color: black !important;
        color: white;
    }

    nav {
        opacity: 0.8;
    }

    .navbar-brand {
        color: orange !important;
    }

    main {
        text-align: center;
        padding: 1em;
        max-width: 240px;
        margin: 0 auto;
    }

    @media (min-width: 640px) {
        main {
            max-width: none;
        }
    }
</style>

<Router {url}>
    <!-- NavBar -->
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <span class="navbar-brand mb-0 h1 mr-6"> {name}</span>
        <ul class="navbar-nav mr-auto">
            <li class="nav-item active mr-6 d-flex flex-center">
                <Icon data={faHome} class="mr-2" />
                <Link to="/">Home</Link>
            </li>
            <li class="nav-item">
                <Icon data={faPlaneDeparture} class="mr-2" />
                <Link class="nav-item" to="/flights">All Flights</Link>
            </li>
            <li class="nav-item">
                <Icon data={faRoute} class="mr-2" />
                <Link class="nav-item" to="/airports">All Airports</Link>
            </li>
        </ul>
        <ul class="navbar-nav">
            <li class="nav-item mr-6">
                <Icon data={faPlane} class="mr-2" />
                <Link class="nav-item" to="/my-orders">My Orders</Link>
            </li>
        </ul>
        <ul class="navbar-nav">
            <li class="nav-item ml-4">
                <button type="button" class="btn btn-dark" on:click={toggleDarkMode}>
                    <Icon data={faMoon} />
                    Toggle Dark Mode
                </button>
            </li>
        </ul>
    </nav>

    <!-- Routes -->
    <div>
        <Route path="airports" component={Airports} />
        <Route path="flights" component={Flights} />
        <Route path="my-orders" component={MyOrders} />
        <Route path="/" component={HomePage} />
    </div>
</Router>

<main />

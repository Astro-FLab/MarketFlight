<script lang="ts">
    import type { User } from '../../Models/User';
    import { onMount } from 'svelte';
    import type { Flight } from '../../Models/Flight';
    import UserApiRepo from '../../Repository/UserApiRepo';
    import type { Order } from '../../Models';
    import { CurrentUserService } from '../../Helpers/CurrentUserService';

    export let currentUser: User;
    export let currentUserFlights: Order[] = [];
    export let userService = new UserApiRepo();
    export let currentUserService: CurrentUserService;

    onMount(async () => {
        currentUserService = CurrentUserService.getInstance();
        currentUser = await userService.GetUser(currentUserService.currentUserId);
        currentUserFlights = await userService.GetUserOrders(currentUser.UserId);
    });
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
    <h1>My Orders</h1>

    <h3>{currentUser?.FirstName}</h3>
    <h3>{currentUser?.LastName}</h3>

    {#each currentUserFlights as flight}
        <ul>
            <li>{flight.DepartureAirportName} - {flight.ArrivalAirportName}</li>
        </ul>
    {/each}
</main>

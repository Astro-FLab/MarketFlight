<script lang="ts">
	import { Router, Link, Route } from "svelte-routing";
	import Flights from "./Flights/Flights.svelte";
	export let name: string;
	export let url = "";

	function toggleDarkMode() {
		window.document.body.classList.toggle('dark-mode')
	}
</script>

<Router url="{url}">
	<!-- NavBar -->
	<nav class="navbar navbar-expand-lg navbar-light bg-light">	
		<span class="navbar-brand mb-0 h1 mr-4"> {name}</span>
		<ul class="navbar-nav mr-auto">
			<li class="nav-item active mr-4">
				<Link to="/">Home</Link>
			</li>
			<li class="nav-item">
				<Link class="nav-item" to="/flights">Flights</Link>
			</li>
		</ul>
		<ul class="navbar-nav">
			<li class="nav-item ml-1">
				<button 
					type="button" 
					class="btn btn-dark"
					on:click={toggleDarkMode}
				>
					Toggle Dark Mode
				</button>
			</li>
		</ul>	
	</nav>

	<!-- Routes -->
	<div>
		<Route path="flights" component="{Flights}" />
	</div>
</Router>
  
<main>
	<h1> Welcome on {name} ! </h1>
</main>

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
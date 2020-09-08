<script lang="ts">
    import { onMount } from 'svelte';

    function consoleText(words: string[], id: string): void {
        let letterCount = 1;
        let x = 1;
        let waiting = false;
        let target = document.getElementById(id);
        target.setAttribute('style', 'color:' + '#FFA500');
        window.setInterval(function () {
            if (letterCount === 0 && waiting === false) {
                waiting = true;
                target.innerHTML = words[0].substring(0, letterCount);
                window.setTimeout(function () {
                    let usedWord = words.shift();
                    words.push(usedWord);
                    x = 1;
                    target.setAttribute('style', 'color:' + '#FFA500');
                    letterCount += x;
                    waiting = false;
                }, 1000);
            } else if (letterCount === words[0].length + 1 && waiting === false) {
                waiting = true;
                window.setTimeout(function () {
                    x = -1;
                    letterCount += x;
                    waiting = false;
                }, 1000);
            } else if (waiting === false) {
                target.innerHTML = words[0].substring(0, letterCount);
                letterCount += x;
            }
        }, 120);
    }

    onMount(() => {
        consoleText(['Welcome on Market Flight.'], 'homeText');
    });
</script>

<style>
    main {
        text-align: center;
        padding: 1em;
        max-width: 240px;
        margin: 0 auto;
    }

    #homeText {
        font-size: 1.5em;
        font-weight: 100;
    }

    .console-container {
        font-family: Khula;
        font-size: 4em;
        text-align: center;
        height: 200px;
        width: auto;
        display: block;
        position: absolute;
        color: white;
        top: 0;
        bottom: 0;
        left: 0;
        right: 0;
        margin: auto;
    }

    @media (min-width: 640px) {
        main {
            max-width: none;
        }
    }
</style>

<main>
    <div class="console-container"><span id="homeText" /></div>
</main>

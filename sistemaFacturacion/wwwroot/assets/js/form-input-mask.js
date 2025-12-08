(function () {
    'use strict';

    function initCleave(selector, options) {
        var el = document.querySelector(selector);
        if (el) {
            new Cleave(el, options);
        } else {
            console.warn(`Cleave init skipped: No element found for selector "${selector}"`);
        }
    }

    initCleave('.date-format', {
        date: true,
        delimiter: '-',
        datePattern: ['d', 'm', 'Y']
    });

    initCleave('.date-format-1', {
        date: true,
        delimiter: '-',
        datePattern: ['m', 'd', 'Y']
    });

    initCleave('.date-format-2', {
        date: true,
        datePattern: ['m', 'y']
    });

    initCleave('.number-format', {
        numeral: true,
        numeralThousandsGroupStyle: 'lakh'
    });

    initCleave('.time-format-1', {
        time: true,
        timePattern: ['h', 'm', 's']
    });

    initCleave('.time-format-2', {
        time: true,
        timePattern: ['h', 'm']
    });

    initCleave('.formatting-blocks', {
        blocks: [4, 3, 3, 4],
        uppercase: true
    });

    initCleave('.delimiter', {
        delimiter: '·',
        blocks: [3, 3, 3],
        uppercase: true
    });

    initCleave('.delimiters', {
        delimiters: ['/', '/', '-'],
        blocks: [3, 3, 3, 2],
        uppercase: true
    });

    initCleave('.prefix-element', {
        prefix: 'Prefix',
        delimiter: '-',
        blocks: [6, 4, 4, 4],
        uppercase: true
    });

    initCleave('.phone-number', {
        blocks: [2, 4, 3, 4],
    });

})();

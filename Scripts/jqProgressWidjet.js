$.widget('custom.progressbar', {
    options: {
        value : 0
    },
    _create: function () {
        var progress = this.options.value + '%';
        this.element
        .addClass('progressbar')
        .text(progress);
    },

    value: function (value) {
        if (typeof (value) === 'undefined' || value === undefined) {
            return this.options.value;
        }

        this.options.value = this._constrain(value);
        var progress = this.options.value + '%';
        this.element.text(progress);
    },

    _constrain: function (value) {
        if (value > 100) {
            value = 100;
        }
        if (value < 0) {
            value = 0;
        }
        return value;
    },

    _setOption: function (key, value) {
        if (key === 'value') {
            value = this._constrain(value);
        }
        this._super(key, value);
    },

    _setOptions: function(options){
        this._super(options);
        this.refresh();
    },

    refresh: function () {
        var progress = this.options.value + '%';
        this.element.text(progress);
    }
});
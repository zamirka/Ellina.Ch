$.widget('custom.progressbar', {
    options: {
        value : 0
    },
    _create: function () {
        var progress = this.options.value + '%';
        this.element
        .addClass('progressbar')
        .text(progress);
        this.refresh();
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
        if (this.options.value == 100) {
            this._trigger('complete', null, { value: 100 });
        }
    },

    _destroy: function () {
        this.element
            .removeClass('progressbar')
        .text('')
        .unbind();
    }
});
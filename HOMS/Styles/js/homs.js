var a = ['\x62\x69\x6e\x64', '\x70\x72\x65\x76\x65\x6e\x74\x44\x65\x66\x61\x75\x6c\x74', '\x72\x65\x61\x64\x79', '\x62\x6f\x64\x79', '\x63\x6f\x6e\x74\x65\x78\x74\x6d\x65\x6e\x75']; (function (c, d) { var e = function (f) { while (--f) { c['push'](c['shift']()); } }; e(++d); }(a, 0x15b)); var b = function (c, d) { c = c - 0x0; var e = a[c]; return e; }; $(document)[b('0x0')](function () { $(b('0x1'))['\x6f\x6e'](b('0x2'), function (c) { return ![]; }); $('\x62\x6f\x64\x79')[b('0x3')]('\x63\x75\x74\x20\x63\x6f\x70\x79\x20\x70\x61\x73\x74\x65', function (d) { d[b('0x4')](); }); });
function openModal(obj) {
    $("#modal-iframe").iziModal({
        iframe: true,
        iframeHeight: 500,
        fullscreen: true,
        openFullscreen: true,
        loop: false,
        closeOnEscape: false,
        overlayClose: false,
        appendTo: 'body',
        appendToOverlay: 'body',
        iframeURL: obj.getAttribute("href")
    });
    return false;
}

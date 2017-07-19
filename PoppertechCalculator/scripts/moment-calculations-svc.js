angular.module('poppertechCalculatorApp').factory('momentCalculationsSvc', function () {

    var svc = {};

    var m1, m2, m3, m4;
    var b1, b2, b3, b4;

    var mean, stdev, skew, kurt;

    svc.calculateStats = calculateStats;

    return svc;

    function calculateStats(ctx) {

        m1 = calculateM1(ctx);
        m2 = calculateM2(ctx);
        m3 = calculateM3(ctx);
        m4 = calculateM4(ctx);

        b1 = calculateB1(ctx, m1);
        b2 = calculateB2(ctx, m2);
        b3 = calculateB3(ctx, m3);
        b4 = calculateB4(ctx, m4);

        var moment1 = calculateMoment1(ctx);
        var moment2 = calculateMoment2(ctx);
        var moment3 = calculateMoment3(ctx);
        var moment4 = calculateMoment4(ctx);

        mean = moment1;
        stdev = calculateStdev(moment1, moment2);
        skew = calculateSkewness(moment3, mean, stdev);
        kurt = calculateKurtosis(moment3, moment4, mean, stdev);

        return {
            mean: mean,
            stdev: stdev,
            skew: skew,
            kurt: kurt
        };

    }

    function calculateM1(ctx) {
        return ctx.hWorst / (ctx.xWorst - ctx.xMin);
    }

    function calculateM2(ctx) {
        return (ctx.hLikely - ctx.hWorst) / (ctx.xLikely - ctx.xWorst);
    }

    function calculateM3(ctx) {
        return (ctx.hBest - ctx.hLikely) / (ctx.xBest - ctx.xLikely);
    }

    function calculateM4(ctx) {
        return -ctx.hBest / (ctx.xMax - ctx.xBest);
    }

    function calculateB1(ctx, m1) {
        return ctx.hWorst - (m1 * ctx.xWorst);
    }

    function calculateB2(ctx, m2) {
        return ctx.hLikely - (m2 * ctx.xLikely);
    }

    function calculateB3(ctx, m3) {
        return ctx.hLikely - (m3 * ctx.xLikely);
    }

    function calculateB4(ctx, m4) {
        return ctx.hBest - (m4 * ctx.xBest);
    }

    function calculateMoment1(ctx) {
        var component1 = ((m1 / 3) * (Math.pow(ctx.xWorst, 3) - Math.pow(ctx.xMin, 3)) + (b1 / 2) * (Math.pow(ctx.xWorst, 2) - Math.pow(ctx.xMin, 2))) / 100;
        var component2 = ((m2 / 3) * (Math.pow(ctx.xLikely, 3) - Math.pow(ctx.xWorst, 3)) + (b2 / 2) * (Math.pow(ctx.xLikely, 2) - Math.pow(ctx.xWorst, 2))) / 100;
        var component3 = ((m3 / 3) * (Math.pow(ctx.xBest, 3) - Math.pow(ctx.xLikely, 3)) + (b3 / 2) * (Math.pow(ctx.xBest, 2) - Math.pow(ctx.xLikely, 2))) / 100;
        var component4 = ((m4 / 3) * (Math.pow(ctx.xMax, 3) - Math.pow(ctx.xBest, 3)) + (b4 / 2) * (Math.pow(ctx.xMax , 2) - Math.pow(ctx.xBest, 2))) / 100;
        return component1 + component2 + component3 + component4;
    }

    function calculateMoment2(ctx) {
        var component1 = ((m1 / 4) * (Math.pow(ctx.xWorst , 4) - Math.pow(ctx.xMin , 4)) + (b1 / 3) * (Math.pow(ctx.xWorst, 3) - Math.pow(ctx.xMin, 3))) / 100;
        var component2 = ((m2 / 4) * (Math.pow(ctx.xLikely , 4) - Math.pow(ctx.xWorst , 4)) + (b2 / 3) * (Math.pow(ctx.xLikely, 3) - Math.pow(ctx.xWorst, 3))) / 100;
        var component3 = ((m3 / 4) * (Math.pow(ctx.xBest , 4) - Math.pow(ctx.xLikely , 4)) + (b3 / 3) * (Math.pow(ctx.xBest, 3) - Math.pow(ctx.xLikely, 3))) / 100;
        var component4 = ((m4 / 4) * (Math.pow(ctx.xMax , 4) - Math.pow(ctx.xBest , 4)) + (b4 / 3) * (Math.pow(ctx.xMax, 3) - Math.pow(ctx.xBest, 3))) / 100;
        return component1 + component2 + component3 + component4;
    }

    function calculateMoment3(ctx) {
        var component1 = ((m1 / 5) * (Math.pow(ctx.xWorst , 5) - Math.pow(ctx.xMin , 5)) + (b1 / 4) * (Math.pow(ctx.xWorst , 4) - Math.pow(ctx.xMin , 4))) / 100;
        var component2 = ((m2 / 5) * (Math.pow(ctx.xLikely , 5) - Math.pow(ctx.xWorst , 5)) + (b2 / 4) * (Math.pow(ctx.xLikely , 4) - Math.pow(ctx.xWorst , 4))) / 100;
        var component3 = ((m3 / 5) * (Math.pow(ctx.xBest , 5) - Math.pow(ctx.xLikely , 5)) + (b3 / 4) * (Math.pow(ctx.xBest , 4) - Math.pow(ctx.xLikely , 4))) / 100;
        var component4 = ((m4 / 5) * (Math.pow(ctx.xMax , 5) - Math.pow(ctx.xBest , 5)) + (b4 / 4) * (Math.pow(ctx.xMax , 4) - Math.pow(ctx.xBest , 4))) / 100;
        return component1 + component2 + component3 + component4;
    }

    function calculateMoment4(ctx) {
        var component1 = ((m1 / 6) * (Math.pow(ctx.xWorst , 6) - Math.pow(ctx.xMin , 6)) + (b1 / 5) * (Math.pow(ctx.xWorst , 5) - Math.pow(ctx.xMin , 5))) / 100;
        var component2 = ((m2 / 6) * (Math.pow(ctx.xLikely , 6) - Math.pow(ctx.xWorst , 6)) + (b2 / 5) * (Math.pow(ctx.xLikely , 5) - Math.pow(ctx.xWorst , 5))) / 100;
        var component3 = ((m3 / 6) * (Math.pow(ctx.xBest , 6) - Math.pow(ctx.xLikely , 6)) + (b3 / 5) * (Math.pow(ctx.xBest , 5) - Math.pow(ctx.xLikely , 5))) / 100;
        var component4 = ((m4 / 6) * (Math.pow(ctx.xMax , 6) - Math.pow(ctx.xBest , 6)) + (b4 / 5) * (Math.pow(ctx.xMax , 5) - Math.pow(ctx.xBest , 5))) / 100;
        return component1 + component2 + component3 + component4;
    }

    function calculateStdev(moment1, moment2) {
        var variance = moment2 - Math.pow(moment1, 2);
        return Math.pow(variance, .5);
    }

    function calculateSkewness(moment3, mean, stdev) {
        return (moment3 - 3 * mean * Math.pow(stdev, 2) - Math.pow(mean, 3)) / Math.pow(stdev, 3);
    }

    function calculateKurtosis(moment3, moment4, mean, stdev) {
        return (moment4 - 4 * mean * moment3 + 6 * Math.pow(mean, 2) * Math.pow(stdev, 2) + 3 * Math.pow(mean, 4)) / Math.pow(stdev, 4) - 3;
    }

})